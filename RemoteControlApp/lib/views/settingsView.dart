import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class SettingsView extends StatelessWidget{
  const SettingsView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        iconTheme: const IconThemeData(color: Colors.white),
        title: const Text(
          'Settings',
          style: TextStyle(color: Colors.grey),
        ),
        centerTitle: false,
        backgroundColor: Color.fromARGB(255,112,119,249),
        systemOverlayStyle: SystemUiOverlayStyle.dark,
      ),
      body:const SizedBox.expand(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.settings,
              size: 100,
            ),
            Text(
              'Settings View',
              style: TextStyle(
                fontSize: 25,
              ),
            ),
            SizedBox(
              height: 8,
            ),
            Padding(
              padding: EdgeInsets.symmetric(horizontal: 30),
              child: Text(
                'In this Settings View Information will be in the future.',
                textAlign: TextAlign.center,
              ),
            )
          ],
        ),
      )
    );
  }
}