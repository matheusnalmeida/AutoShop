import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/veiculo.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'veiculo_event.dart';
part 'veiculo_state.dart';

class VeiculoBloc extends Bloc<VeiculoEvent, VeiculoState> {
  final VeiculoRepository repository;

  VeiculoBloc(this.repository) : super(const LoadingState()){
    on<GetAllVeiculosEvent>(_onPostFetched);
  }

  Future<void> _onPostFetched(VeiculoEvent event, Emitter<VeiculoState> emit) async {
    try {
      emit(const LoadingState());
      var albums = (await repository.fetchAllVeiculos());
      return emit(LoadedSucessState(albums));
    } on HttpException catch (ex) {
      return emit(ErrorState(ex.message));
    } catch (_){
        return emit(const ErrorState("Erro ao tentar obter os veiculos!"));
    }
  }
}
