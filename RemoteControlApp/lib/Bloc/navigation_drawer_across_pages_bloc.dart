import 'package:bloc/bloc.dart';
import 'package:dfm_remote_control/DataModels/NavigationItemModel.dart';
import 'package:equatable/equatable.dart';


part 'navigation_drawer_across_pages_event.dart';
part 'navigation_drawer_across_pages_state.dart';

class NavigationDrawerAcrossPagesBloc extends Bloc<NavigationDrawerAcrossPagesEvent, NavigationDrawerAcrossPagesState> {
  NavigationDrawerAcrossPagesBloc() : super(const NavigationDrawerAcrossPagesState(selectedItem: changePage.loginView)) {
    on<NavigateTo>((event, emit) {
      if(event.destination != state.selectedItem){
        emit(NavigationDrawerAcrossPagesState(selectedItem: event.destination));
      }
    });
  }
}
