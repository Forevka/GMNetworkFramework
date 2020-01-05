///client_raw_send_ping();

//Sends ping constant.
var buffer = buffer_create(256, buffer_grow, 1);
buffer_seek(buffer, buffer_seek_start, 0);
buffer_write(buffer , buffer_u16, 2004);


network_send_raw(socket, buffer, buffer_tell(buffer));

//Delete the buffer.
buffer_delete(buffer);
