import 'package:flutter/material.dart';

class AboutView extends StatefulWidget{
  const AboutView({super.key});

  @override
  State<AboutView> createState() => _AboutViewState();

}

class _AboutViewState extends State<AboutView>{
  @override
  Widget build(BuildContext context) {
   return Scaffold(
     appBar: AppBar(
      title: const Text('About:'),
        automaticallyImplyLeading: false,
     ),
     body: Center(
       child: Column (
         mainAxisAlignment: MainAxisAlignment.center,
         children: [
           Table(
             border: TableBorder.all(),
             columnWidths: const{
               0: FixedColumnWidth(500)
             },
             children: const [
               TableRow(children: [
                  TableCell(child: Center(child: Text("System Name : ADT Remote Control Application"),))
               ]),
               TableRow(children: [
                 TableCell(child: Center(child: Text("version: 0.0.1"),))
               ])
             ],
           )
         ],
       ),
     ),
   );
  }
}