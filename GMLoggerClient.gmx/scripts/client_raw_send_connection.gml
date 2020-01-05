///client_raw_send_connection();

//Create variables.
var ip_address = global.ip_address;

//Sends account data.
var buffer = buffer_create(256, buffer_grow, 1);
buffer_seek(buffer, buffer_seek_start, 0);
buffer_write(buffer , buffer_u16, 2000);
buffer_write(buffer, buffer_string, ip_address);

network_send_raw(socket, buffer, buffer_tell(buffer));


//Delete the buffer.
buffer_delete(buffer);
