///network_invoke_handler(me: instance, flag: ushort, buffer: buffer)
var _h = ds_map_find_value(global.network_proccesing_functions, argument[1])
if (!is_undefined(_h))
{
    script_execute(_h, argument[0], argument[2])
}
