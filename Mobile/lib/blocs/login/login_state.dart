part of 'login_bloc.dart';

abstract class LoginState extends Equatable {
  const LoginState();
  
  @override
  List<Object> get props => [];
}

class LoginInitial extends LoginState {}

class LoginLoadingState extends LoginState {
  const LoginLoadingState();
  @override
  List<Object> get props => [];
}

class LoginLoadedSucessState extends LoginState {
  final Login login;
  const LoginLoadedSucessState(this.login);
  @override
  List<Object> get props => [login];
}

class LoginErrorState extends LoginState {
  final String message;
  const LoginErrorState(this.message);
  @override
  List<Object> get props => [message];
}