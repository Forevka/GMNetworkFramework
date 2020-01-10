///client_raw_send_ping();

//Sends ping constant.
var buffer = buffer_create(256, buffer_grow, 1); //create buffer
buffer_seek(buffer, buffer_seek_start, 0); //pointer at start
buffer_write(buffer , buffer_u16, 2004); //code 2004 on server listen method
buffer_write(buffer , buffer_string, string(GetCurrentTimestamp()));//write float current datetime in unix format

show_debug_message("sending ping")
network_send_raw(socket, buffer, buffer_tell(buffer));

//Delete the buffer.
buffer_delete(buffer);
