///network_register_handler(flag: ushort, callback: function|script)
//callback MUST have arguments:
//  0 - who calls e.g. self
//  1 - buffer

ds_map_add(global.network_proccesing_functions, argument[0], argument[1])
