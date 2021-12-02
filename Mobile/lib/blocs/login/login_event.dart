part of 'login_bloc.dart';

abstract class LoginEvent extends Equatable {
  const LoginEvent();

  @override
  List<Object> get props => [];
}

class CreateLoginEvent extends LoginEvent {
  final Login login;
  const CreateLoginEvent(this.login);

  @override
  List<Object> get props => [login];
}
