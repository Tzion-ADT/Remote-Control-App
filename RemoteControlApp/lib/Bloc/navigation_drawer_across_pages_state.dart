part of 'navigation_drawer_across_pages_bloc.dart';

/// Represents the state of the navigation drawer with the selected page.
class NavigationDrawerAcrossPagesState extends Equatable {
  final changePage selectedItem;

  /// Initializes a new instance of the [NavigationDrawerAcrossPagesState] with the given [selectedItem].
  const NavigationDrawerAcrossPagesState({required this.selectedItem});

  @override
  List<Object?> get props => [selectedItem];
}
