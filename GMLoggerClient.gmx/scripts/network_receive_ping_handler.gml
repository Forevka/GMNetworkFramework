///network_receive_ping_handler

var me = argument[0];
var buf = argument[1];

var buffer = buffer_create(128, buffer_grow, 1);
buffer_seek(buffer, buffer_seek_start, 0);
buffer_write(buffer , buffer_u16, 2005);

//Return ping to client.
network_send_raw(socket, buffer, buffer_tell(buffer));
buffer_delete(buffer);
