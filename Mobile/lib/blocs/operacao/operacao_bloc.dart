import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/operacao/operacao.dart';
import 'package:autoshop_application/models/operacao/operacao_create.dart';
import 'package:autoshop_application/repositories/operacao/operacao_repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'operacao_event.dart';
part 'operacao_state.dart';

class OperacaoBloc extends Bloc<OperacaoEvent, OperacaoState> {
  final OperacaoRepository repository;

  OperacaoBloc(this.repository) : super(const OperacaoLoadingState()) {
    on<GetAllOperacoesEvent>(_onPostFetched);
    on<CreateOperacaoEvent>(_onCreate);
    on<UpdateOperacaoEvent>(_onUpdate);
    //on<DeleteOperacaoEvent>(_onDelete);
  }

  Future<void> _onPostFetched(
      GetAllOperacoesEvent event, Emitter<OperacaoState> emit) async {
    try {
      emit(const OperacaoLoadingState());
      var result = (await repository.fetchAllOperacoes());
      return emit(OperacaoLoadedSucessState(result));
    } on HttpException catch (ex) {
      return emit(OperacaoErrorState(ex.message));
    } catch (_) {
      return emit(const OperacaoErrorState("Erro ao tentar obter os operação!"));
    }
  }

  Future<void> _onCreate(
      CreateOperacaoEvent event, Emitter<OperacaoState> emit) async {
    try {
      emit(const OperacaoLoadingState());
      var result = await repository.createOperacao(event.operacao);
      if (result.sucesso) {
        add(GetAllOperacoesEvent());
        return;
      }
      emit(OperacaoErrorState(result.mensagens[0]));
    } on HttpException catch (ex) {
      return emit(OperacaoErrorState(ex.message));
    } catch (_) {
      return emit(const OperacaoErrorState("Erro ao tentar cadastrar operação!"));
    }
  }

  Future<void> _onUpdate(
      UpdateOperacaoEvent event, Emitter<OperacaoState> emit) async {
    try {
      emit(const OperacaoLoadingState());
      var result = await repository.updateOperacao(event.operacao);
      if (result.sucesso) {
        add(GetAllOperacoesEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(OperacaoErrorState(ex.message));
    } catch (_) {
      return emit(const OperacaoErrorState("Erro ao tentar atualizar operação!"));
    }
  }
}
