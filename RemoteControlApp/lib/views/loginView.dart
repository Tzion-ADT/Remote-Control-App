import 'dart:io';

import 'package:dfm_remote_control/ClientSide/ClientCommunication.dart';
import 'package:flutter/material.dart';

import 'machineView.dart';

class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<StatefulWidget> createState() => LoginPageState();
}

class LoginPageState extends State<LoginPage> {
  final List<String> _dropdownItems = ['ADT Administrator', 'Administrator', 'Engineer', 'Operator'];
  String? _selectedItem;
  final _formKey = GlobalKey<FormState>();
  final TextEditingController _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('ADT Login'),
      ),
      body: Center(
        child: Padding(
          padding: const EdgeInsets.all(20),
          child: Form(
            key: _formKey,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                DropdownButtonFormField<String>(
                  hint: const Text('Select an option'),
                  items: _dropdownItems.map((String item) {
                    return DropdownMenuItem<String>(
                      value: item,
                      child: Text(item),
                    );
                  }).toList(),
                  onChanged: (String? newValue) {
                    setState(() {
                      _selectedItem = newValue;
                    });
                  },
                  value: _selectedItem,
                  validator: (value) {
                    if (value == null) {
                      return 'Please select an option';
                    }
                    return null;
                  },
                ),

                const SizedBox(height: 16.0),
                TextFormField(
                  controller: _passwordController,
                  validator: (value) {
                    if (value == null || value.isEmpty) {
                      return 'Please enter your password';
                    }
                    return null;
                  },
                  decoration: const InputDecoration(
                    labelText: 'Password',
                  ),
                  obscureText: true,
                ),
                const SizedBox(height: 16.0),
                ElevatedButton(
                  onPressed: () {
                    if (_formKey.currentState!.validate()) {
                      // If the form is valid, submit the data
                      openMachineView(_selectedItem);
                    }
                  },
                  child: const Text('Submit'),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }

  Future<void> openMachineView(var user) async {
    if(_submitForm(_selectedItem)) {

      Navigator.push(context, MaterialPageRoute(builder: (context) => MachineView(userName: _selectedItem.toString())));
      //Navigator.push(context, MaterialPageRoute(builder: (context) => ClientCommunication()));
    } else {
      showDialog(
          context: context,
          builder: (context) => AlertDialog(
            title: const Text('Wrong password'),
            actions: [
              TextButton(
                  onPressed:(){ Navigator.pop(context);},
                  child: const Text('OK'))
            ],
          ));
    }
  }

  bool _submitForm(var user) {
    // Handle form submission logic here
    String password = _passwordController.text;

    // For demonstration purposes, just print the values
    print('User: $user');
    print('Password: $password');

    if(passwordValidation(user , password))
      {return true;}
    return false;
  }

  bool passwordValidation(var user , String password){
    final List<String> users = ['ADT Administrator', 'Administrator', 'Engineer', 'Operator'];
    switch(user){
      case "ADT Administrator":
          if(password == "sw${generatePasswordSuffix()}"){return true;}
          break;
      case "Administrator":
          if(password == "a"){return true;}
          break;
      case "Engineer":
          if(password == "e"){return true;}
          break;
      case "Operator":
          if(password == "o"){return true;}
          break;
    }

    return false;
  }

  //password for ADT Administrator
  String generatePasswordSuffix()
  {
    int passwordSuffix;

    DateTime dt = DateTime.now();
    int year = dt.year%100;
    int month = dt.month;
    int day = dt.day;

    passwordSuffix = 1000 * year + day * day * month * month + day * day * day;

    return passwordSuffix.toString();
  }
}
