import 'package:dfm_remote_control/views/loginView.dart';
import 'package:dfm_remote_control/views/settingsView.dart';
import 'package:dfm_remote_control/views/mainView.dart';
import 'package:dfm_remote_control/DataModels/NavigationItemModel.dart';
import 'package:flutter/cupertino.dart';


Widget NavigateToWidget(changePage item) {
  switch (item) {
    case changePage.loginView:
      return const LoginPage();
    case changePage.settingsView:
      return const SettingsView();

    default:
      return const MainView(); // Default page if item is not recognized
  }
}