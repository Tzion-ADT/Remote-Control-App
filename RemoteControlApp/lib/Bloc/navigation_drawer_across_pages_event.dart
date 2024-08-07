part of 'navigation_drawer_across_pages_bloc.dart';


sealed class NavigationDrawerAcrossPagesEvent extends Equatable {
  const NavigationDrawerAcrossPagesEvent();

  @override
  List<Object> get prop =>[];

}

class NavigateTo extends NavigationDrawerAcrossPagesEvent{
  final changePage destination;

  const NavigateTo(this.destination);

  @override
  List<Object?> get props => [destination];
}
