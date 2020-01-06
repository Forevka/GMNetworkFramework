///network_system_raw_connected

show_debug_message("CONNECTED!")
var success = ds_map_find_value(async_load, "succeeded");
if (success = 0)
{            
    //Failure connection. Retry.
    global.connected_to_server = false;
}
else
{
    //Succesful connection.
    global.connected_to_server = true;
    client_raw_send_connection();
    client_raw_send_ping();
}
