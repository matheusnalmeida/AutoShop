part of 'usuario_bloc.dart';

abstract class UsuarioState extends Equatable {
  const UsuarioState();
  
  @override
  List<Object> get props => [];
}

class UsuarioInitial extends UsuarioState {}

class UsuarioLoadingState extends UsuarioState {
  const UsuarioLoadingState();
  @override
  List<Object> get props => [];
}

class UsuarioLoadedSucessState extends UsuarioState {
  final List<Usuario?> usuarios;
  const UsuarioLoadedSucessState(this.usuarios);
  @override
  List<Object> get props => [usuarios];
}

class UsuarioErrorState extends UsuarioState {
  final String message;
  const UsuarioErrorState(this.message);
  @override
  List<Object> get props => [message];
}