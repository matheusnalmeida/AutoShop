import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/veiculo/veiculo.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'veiculo_event.dart';
part 'veiculo_state.dart';

class VeiculoBloc extends Bloc<VeiculoEvent, VeiculoState> {
  final VeiculoRepository repository;

  VeiculoBloc(this.repository) : super(const VeiculoLoadingState()) {
    on<GetAllVeiculosEvent>(_onPostFetched);
    on<CreateVeiculoEvent>(_onCreate);
    on<UpdateVeiculoEvent>(_onUpdate);
    on<DeleteVeiculoEvent>(_onDelete);
  }

  Future<void> _onPostFetched(
      GetAllVeiculosEvent event, Emitter<VeiculoState> emit) async {
    try {
      emit(const VeiculoLoadingState());
      var result = (await repository.fetchAllVeiculos());
      return emit(VeiculoLoadedSucessState(result));
    } on HttpException catch (ex) {
      return emit(VeiculoErrorState(ex.message));
    } catch (_) {
      return emit(const VeiculoErrorState("Erro ao tentar obter os veiculos!"));
    }
  }

  Future<void> _onCreate(
      CreateVeiculoEvent event, Emitter<VeiculoState> emit) async {
    try {
      emit(const VeiculoLoadingState());
      var result = await repository.createVeiculo(event.veiculo);
      if (result.sucesso) {
        add(GetAllVeiculosEvent());
        return;
      }
      emit(VeiculoErrorState(result.mensagens[0]));
    } on HttpException catch (ex) {
      return emit(VeiculoErrorState(ex.message));
    } catch (_) {
      return emit(const VeiculoErrorState("Erro ao tentar cadastrar veiculo!"));
    }
  }

  Future<void> _onUpdate(
      UpdateVeiculoEvent event, Emitter<VeiculoState> emit) async {
    try {
      emit(const VeiculoLoadingState());
      var result = await repository.updateVeiculo(event.veiculo);
      if (result.sucesso) {
        add(GetAllVeiculosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(VeiculoErrorState(ex.message));
    } catch (_) {
      return emit(const VeiculoErrorState("Erro ao tentar atualizar veiculo!"));
    }
  }

  Future<void> _onDelete(
      DeleteVeiculoEvent event, Emitter<VeiculoState> emit) async {
    try {
      emit(const VeiculoLoadingState());
      var result = await repository.deleteVeiculo(event.veiculo.id!);
      if (result.sucesso) {
        add(GetAllVeiculosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(VeiculoErrorState(ex.message));
    } catch (_) {
      return emit(const VeiculoErrorState("Erro ao tentar remover veiculo!"));
    }
  }
}
