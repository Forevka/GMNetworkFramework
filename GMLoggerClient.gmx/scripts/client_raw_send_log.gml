///client_raw_send_log(msg);

//Create variables.
var msg = argument[0];

//Sends account data.
var buffer = buffer_create(256, buffer_grow, 1);
buffer_seek(buffer, buffer_seek_start, 0);
buffer_write(buffer , buffer_u16, 2006);
buffer_write(buffer, buffer_string, msg);

show_debug_message("sending log info")
network_send_raw(socket, buffer, buffer_tell(buffer));


//Delete the buffer.
buffer_delete(buffer);
