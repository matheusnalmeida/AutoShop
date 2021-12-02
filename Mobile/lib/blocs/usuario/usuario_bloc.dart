import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/models.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'usuario_event.dart';
part 'usuario_state.dart';

class UsuarioBloc extends Bloc<UsuarioEvent, UsuarioState> {
  final UsuarioRepository repository;

  UsuarioBloc(this.repository) : super(const UsuarioLoadingState()) {
    on<GetAllUsuariosEvent>(_onPostFetched);
    on<CreateUsuarioEvent>(_onCreate);
    on<UpdateUsuarioEvent>(_onUpdate);
    on<DeleteUsuarioEvent>(_onDelete);
  }

  Future<void> _onPostFetched(
      GetAllUsuariosEvent event, Emitter<UsuarioState> emit) async {
    try {
      emit(const UsuarioLoadingState());
      var result = (await repository.fetchAllUsuarios());
      return emit(UsuarioLoadedSucessState(result));
    } on HttpException catch (ex) {
      return emit(UsuarioErrorState(ex.message));
    } catch (_) {
      return emit(const UsuarioErrorState("Erro ao tentar obter os usuarios!"));
    }
  }

  Future<void> _onCreate(
      CreateUsuarioEvent event, Emitter<UsuarioState> emit) async {
    try {
      emit(const UsuarioLoadingState());
      var result = await repository.createUsuario(event.usuario);
      if (result.sucesso) {
        add(GetAllUsuariosEvent());
        return;
      }
      emit(UsuarioErrorState(result.mensagens[0]));
    } on HttpException catch (ex) {
      return emit(UsuarioErrorState(ex.message));
    } catch (_) {
      return emit(const UsuarioErrorState("Erro ao tentar cadastrar usuario!"));
    }
  }

  Future<void> _onUpdate(
      UpdateUsuarioEvent event, Emitter<UsuarioState> emit) async {
    try {
      emit(const UsuarioLoadingState());
      var result = await repository.updateUsuario(event.usuario);
      if (result.sucesso) {
        add(GetAllUsuariosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(UsuarioErrorState(ex.message));
    } catch (_) {
      return emit(const UsuarioErrorState("Erro ao tentar atualizar usuario!"));
    }
  }

  Future<void> _onDelete(
      DeleteUsuarioEvent event, Emitter<UsuarioState> emit) async {
    try {
      emit(const UsuarioLoadingState());
      var result = await repository.deleteUsuario(event.usuario.id!);
      if (result.sucesso) {
        add(GetAllUsuariosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(UsuarioErrorState(ex.message));
    } catch (_) {
      return emit(const UsuarioErrorState("Erro ao tentar remover usuario!"));
    }
  }
}
