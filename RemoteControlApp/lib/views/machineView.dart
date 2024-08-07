import 'dart:io';
import 'package:flutter/material.dart';

class MachineView extends StatefulWidget {
  final String userName;

  const MachineView({super.key, required this.userName});

  @override
  State<StatefulWidget> createState() => MachineViewState(userName);
}

class MachineViewState extends State<MachineView> {
  final String userName;
  Socket? _socket;

  MachineViewState(this.userName);

  void _connectToServer() async {
    try {
      final InternetAddress serverAddress = InternetAddress('199.203.44.132');
      const port = 1999;
      _socket = await Socket.connect(serverAddress, port);

      // Listen for data from the server
      _socket!.listen((List<int> data) {
        // Convert byte data to string and print
        String message = String.fromCharCodes(data);
        print('Server: $message');
        setState(() {
          // Update UI or state with the message if needed
        });
      });

      // Optionally send data back to the server
      // _socket!.write('Hello from client!');

    } catch (e) {
      print("Unable to connect: $e");
    }
  }

  @override
  void initState() {
    super.initState();
    _connectToServer(); // Call the connect method when the widget is initialized
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Welcome ${userName} !!'),
        automaticallyImplyLeading: false,
      ),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Table(
              border: TableBorder.all(),
              children: const [
                TableRow(children: [
                  TableCell(child: Center(child: Text('Header 1'))),
                  TableCell(child: Center(child: Text('Header 2'))),
                  TableCell(child: Center(child: Text('Header 3'))),
                ]),
                TableRow(children: [
                  TableCell(child: Center(child: Text('Row 1, Col 1'))),
                  TableCell(child: Center(child: Text('Row 1, Col 2'))),
                  TableCell(child: Center(child: Text('Row 1, Col 3'))),
                ]),
                TableRow(children: [
                  TableCell(child: Center(child: Text('Row 2, Col 1'))),
                  TableCell(child: Center(child: Text('Row 2, Col 2'))),
                  TableCell(child: Center(child: Text('Row 2, Col 3'))),
                ]),
              ],
            ),
            // const SizedBox(height: 20), // Add some space between the table and the image
            // Image.asset(
            //   'assets/8030.png',
            //   width: 200,
            //   height: 200,
            // ),
          ],
        ),
      ),
      //   <--- image
    );
  }

  @override
  void dispose() {
    _socket?.close(); // Close the socket connection when the widget is disposed
    super.dispose();
  }
}
