part of 'usuario_bloc.dart';

abstract class UsuarioEvent extends Equatable {
  const UsuarioEvent();

  @override
  List<Object> get props => [];
}

class GetAllUsuariosEvent extends UsuarioEvent {
  @override
  List<Object> get props => [];
}

class GetUsuarioEvent extends UsuarioEvent {
  final String id;
  const GetUsuarioEvent(this.id);
  
  @override
  List<Object> get props => [id];
}

class CreateUsuarioEvent extends UsuarioEvent {
  final Usuario usuario;
  const CreateUsuarioEvent(this.usuario);

  @override
  List<Object> get props => [usuario];
}

class UpdateUsuarioEvent extends UsuarioEvent {
  final Usuario usuario;
  const UpdateUsuarioEvent(this.usuario);

  @override
  List<Object> get props => [usuario];
}

class DeleteUsuarioEvent extends UsuarioEvent {
  final Usuario usuario;

  const DeleteUsuarioEvent(this.usuario);

  @override
  List<Object> get props => [usuario];
}