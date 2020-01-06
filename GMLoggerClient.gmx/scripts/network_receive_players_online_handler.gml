///network_receive_players_online_handler

var me = argument[0];
var buf = argument[1];

var players_online = buffer_read(buf, buffer_s32);
               
//Update global.
global.players_online = players_online;
