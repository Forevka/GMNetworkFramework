﻿using GMNetworkFramework.Server.Utils.Attributes;
using GMNetworkFramework.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMNetworkFramework.Example.Models.Response
{
    public class PlayersCountModelResponse : BaseResponseModel
    {
        [Position(0)]
        public int Count { get; set; }
    }
}