import 'dart:io';

import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class ClientCommunication extends StatefulWidget {
    @override
    _TcpClientExampleState createState() => _TcpClientExampleState();
  }
  

class _TcpClientExampleState extends State<ClientCommunication> {
  Socket? _socket;
  String _message = "temp";
  
  @override
  initState(){
    super.initState();
    _connectToServer();

  }
void _connectToServer() async{
    try {
      final InternetAddress serverAddress = InternetAddress('199.203.44.132');
      const port = 1999;
      _socket = await Socket.connect(serverAddress , port);


      _socket!.listen((List<int> data) {
        setState((){
          _message = String.fromCharCode(data as int);
        });
      });

      //send data back to the server
      _sendMessage("Hello from client flutter");
    }catch(e){
      print("Unable to connect: $e");
    }
}

void _sendMessage(String message){
    if(_socket != null){
      _socket!.write(message);
    }
}

  @override
  void dispose() {
    _socket?.close();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold( appBar: AppBar(
      title: Text('Connect'),
      automaticallyImplyLeading: false,)
    );
  }
}