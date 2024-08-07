import 'package:dfm_remote_control/Bloc/navigation_drawer_across_pages_bloc.dart';
import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import '../DataModels/NavigationItemModel.dart';
import 'NavigateToWidget.dart';

class NavigationDrawerAcrossPagesWidget extends StatefulWidget{
  const NavigationDrawerAcrossPagesWidget({super.key});

  @override
  State<StatefulWidget> createState() => _NavigationDrawerAcrossPagesWidgetState();
}

class _NavigationDrawerAcrossPagesWidgetState extends State<NavigationDrawerAcrossPagesWidget>{

  @override
  final List<NavigationItem> _navigationItems = [
    NavigationItem(
        changePage.loginView,
        "Login",
        Icons.home,
    ),
    NavigationItem(
        changePage.settingsView,
        "Settings",
        Icons.military_tech,
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return Drawer(
      child: Column(
        children: [
          Image.asset('assets/adt_logo.png'),
          /*const UserAccountsDrawerHeader(
              accountName: Text(
                'testName',
                style: TextStyle(color: Colors.black)
                ),
              accountEmail:  Text(
              'testName@test.com',
              style: TextStyle(color: Colors.black)
            ),
          ),*/
          Expanded(
                  ///The List of items
                  child :ListView.builder(
                      padding: EdgeInsets.zero,
                      itemCount: _navigationItems.length,
                      shrinkWrap: true,
                      itemBuilder: (ctx , i) {
                        return BlocBuilder<NavigationDrawerAcrossPagesBloc , NavigationDrawerAcrossPagesState>(
                          buildWhen: (previous , current){
                            return previous.selectedItem != current.selectedItem;
                          },
                          builder:(context , state) =>
                              _buildItem(_navigationItems[i] , state)
                        );
                  })
          )
        ],
      ),
    );
  }

  //Build each DrawerItem
  Widget _buildItem(NavigationItem data , NavigationDrawerAcrossPagesState state) =>
      _makeListItems(data , state);

  Widget _makeListItems(NavigationItem data , NavigationDrawerAcrossPagesState state){
    return Card(
        color: Colors.grey[100],
        shape: const ContinuousRectangleBorder(
          borderRadius: BorderRadius.zero,
        ),
      borderOnForeground: true,
      elevation: 0,
      margin: EdgeInsets.zero,
      child: ListTile(
        title: Text(
          data.title,
          style: TextStyle(
            fontWeight: data.item == state.selectedItem
                ? FontWeight.bold
                :FontWeight.w300,
            color: data.item == state.selectedItem
              ?const Color.fromARGB(255,112,119,249)
                :Colors.grey[600]
          ),
        ),
        leading: Icon(
          data.icon,
          color: data.icon == state.selectedItem
            ? const Color.fromARGB(255, 112, 119, 249)
            : Colors.grey[600],
        ),
        onTap: () {
          /*Debug
          print("Tapping on item: ${data.item}");
          print("Context widget: ${context.widget}");
          */
          _handleItemClick(context, data.item);
        },
      ),
    );
  }

  void _handleItemClick(BuildContext context , changePage item){
    // Add navigation event to the Bloc
    BlocProvider.of<NavigationDrawerAcrossPagesBloc>(context).add(NavigateTo(item));

    // Close the drawer
    Navigator.pop(context);

    // Decide which page to navigate to based on 'item'
    Widget page = NavigateToWidget(item); // You need to implement this function

    // Navigate to the specific view
    Navigator.push(context, MaterialPageRoute(builder: (context) => page));
  }
}
