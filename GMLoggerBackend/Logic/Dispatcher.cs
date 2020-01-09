using GMNetworkFramework.Server.Handlers;
using GMNetworkFramework.Server.Enums;
using GMNetworkFramework.Server.Exceptions;
using GMNetworkFramework.Server.Helpers;
using GMNetworkFramework.Server.Middlewares;
using GMNetworkFramework.Server.Models;
using GMNetworkFramework.Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Server.Logic
{
    public class Dispatcher
    {
        public bool isWorking = true;
        public string Name;

        private Dictionary<ushort, List<IHandler>> Handlers = new Dictionary<ushort, List<IHandler>>();
        private List<Type> MiddlewaresABC = new List<Type>();

        private List<IMiddleware> Middlewares = new List<IMiddleware>();

        private SortedList<int, Dispatcher> slavesDispatchers = new SortedList<int, Dispatcher>();
        public Guid Id = Guid.NewGuid();

        public Dispatcher(string name)
        {
            //AttachDispatcher(this); //self add
            Name = name;
        }

        public void RegisterHandler(ushort flag, IHandler handler)
        {
            List<IHandler> hList = null;

            if (Handlers.TryGetValue(flag, out hList))
                hList.Add(handler);
            else
            {
                Handlers.Add(flag, new List<IHandler>() { handler });
            }
        }

        public void RegisterMiddleware(Type middleware)
        {
            if (!middleware.GetInterfaces().Contains(typeof(IMiddleware)))
                throw new InvalidOperationException($"Can`t add {middleware.Name}. He dont inherit interface IMiddleware");

            Logger.Debug(middleware.ToString());
            MiddlewaresABC.Add(middleware);
        }

        public void AttachDispatcher(Dispatcher newDispatcher)
        {
            if (newDispatcher == this)
                throw new InvalidOperationException("Cant attach dispatcher to self!");

            slavesDispatchers.Add(slavesDispatchers.Count, newDispatcher);
        }

        public Dictionary<Guid, List<IMiddleware>> InitializeMiddlewares()
        {
            var middlewaresInstances = new Dictionary<Guid, List<IMiddleware>>();

            middlewaresInstances.Add(Id, InstanciateMiddlewares());

            foreach(var disp in slavesDispatchers.Values)
            {
                middlewaresInstances.Add(disp.Id, disp.InstanciateMiddlewares());
            }

            return middlewaresInstances;
        }

        private List<IMiddleware> InstanciateMiddlewares()
        {
            return MiddlewaresABC.Select(x => { var a = (IMiddleware)Activator.CreateInstance(x); a.OnStart(this); return a; }).ToList();
        }

        private bool InvokeHandlers(BaseRequestModel model, UserModel user, SocketHelper socket, Dictionary<string, string> data)
        {
            var handled = false;

            if (Handlers.TryGetValue(model.Flag, out List<IHandler> hList))
            {
                handled = true;
                bool isStop = false;
                hList.ForEach(x =>
                {
                    try
                    {
                        if (!isStop)
                        {
                            Logger.Debug($"Processing {x.GetType().Name}");
                            x.Process(model, user, socket, data);
                        }
                    }
                    catch (CancelHandlerException)
                    {
                        Logger.Debug($"{x.GetType().Name} skipped");
                    }
                    catch (StopProcessingException)
                    {
                        isStop = true;
                        Logger.Debug($"Invoking handlers stopped by {x.GetType().Name}");
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex, "Error while invoking handlers");
                    }
                });
            }
            /*else
            {
                var unhandled = new Unhandled();
                unhandled.Process(model, user, socket, data);
            }*/
            return handled;
        }

        private bool Invoke(BaseRequestModel model, UserModel user, SocketHelper socket, Dictionary<string, string> data)
        {
            var middlewaredForThisDispatcher = socket.myMiddlewares
                .Where(x => x.Key == Id)
                .Select(xx => xx.Value)
                .SelectMany(x => x.FindAll(m =>
                                            m.Flags.Contains((ushort)RequestFlag.ForAll) ||
                                            m.Flags.Contains(model.Flag)));


            //Logger.Debug($"{Name} PreProcess midlewares start");
            foreach (var middleware in middlewaredForThisDispatcher)
            {
                middleware.PreProcess(model, user, socket, data);
            }
            //Logger.Debug($"{Name} PreProcess midlewares finish");

            //Logger.Debug($"{Name} Handlers start");
            var handled = InvokeHandlers(model, user, socket, data);
            //Logger.Debug($"{Name} Handlers finish");

            //Logger.Debug($"{Name} PostProcess midlewares start");
            foreach (var middleware in middlewaredForThisDispatcher)
            {
                middleware.PostProcess(model, user, socket, data);
            }
            //Logger.Debug($"{Name} PostProcess midlewares finish");
            
            
            return handled;
        }

        public void Handle(BaseRequestModel model, UserModel user, SocketHelper socket)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            var handled = Invoke(model, user, socket, data);

            foreach (var disp in slavesDispatchers.Values.Where(x => x.isWorking == true))
            {
                //Logger.Debug($"{Name} dispatcher is proccessing");
                handled = disp.Invoke(model, user, socket, data);
            }

            if (!handled)
            {
                if (Handlers.TryGetValue((ushort)RequestFlag.Unhandled, out List<IHandler> hList))
                {
                    //var unhandled = new Unhandled();
                    //unhandled.Process(model, user, socket, data);
                    bool isStop = false;
                    hList.ForEach(x =>
                    {
                        try
                        {
                            if (!isStop)
                            {
                                Logger.Debug($"Processing {x.GetType().Name}");
                                x.Process(model, user, socket, data);
                            }
                        }
                        catch (CancelHandlerException)
                        {
                            Logger.Debug($"{x.GetType().Name} skipped");
                        }
                        catch (StopProcessingException)
                        {
                            isStop = true;
                            Logger.Debug($"Invoking handlers stopped by {x.GetType().Name}");
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex, "Error while invoking handlers");
                        }
                    });
                }
            }
        }
    }
}
