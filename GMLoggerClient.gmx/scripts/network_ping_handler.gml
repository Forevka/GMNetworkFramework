///network_ping_handler(self: instance, buffer: buffer)

var me = argument[0];
var buf = argument[1];

//Update variables.
me.ping = ping_step;
alarm[0] = 1 * room_speed * 0.5//room_seconds(1); //Send again in 1 second.
var msg = buffer_read(buf, buffer_string);
var test_float = buffer_read(buf, buffer_f32);
show_debug_message("received ping " + msg)
show_debug_message(test_float)
