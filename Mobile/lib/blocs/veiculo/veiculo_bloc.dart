import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/veiculo.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'veiculo_event.dart';
part 'veiculo_state.dart';

class VeiculoBloc extends Bloc<VeiculoEvent, VeiculoState> {
  final VeiculoRepository repository;

  VeiculoBloc(this.repository) : super(const LoadingState());

  @override
  Stream<VeiculoState> mapEventToState(
    VeiculoEvent event,
  ) async* {
    yield const LoadingState();
    if (event is GetAllVeiculosEvent) {
      yield* _mapAlbumsLoadedToState();
    } 
  }

  Stream<VeiculoState> _mapAlbumsLoadedToState() async* {
    try {
      var albums = (await repository.fetchAllVeiculos());
      yield LoadedSucessState(albums);
    } on HttpException catch (ex) {
      yield ErrorState(ex.message);
    } catch (_){
        yield const ErrorState("Erro ao tentar obter os veiculos!");
    }
  }
}
