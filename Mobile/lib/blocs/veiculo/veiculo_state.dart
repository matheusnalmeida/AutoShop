part of 'veiculo_bloc.dart';

abstract class VeiculoState extends Equatable {
  const VeiculoState();
  
  @override
  List<Object> get props => [];
}

class VeiculoInitial extends VeiculoState {}

class VeiculoLoadingState extends VeiculoState {
  const VeiculoLoadingState();
  @override
  List<Object> get props => [];
}

class VeiculoLoadedSucessState extends VeiculoState {
  final List<Veiculo> veiculos;
  const VeiculoLoadedSucessState(this.veiculos);
  @override
  List<Object> get props => [veiculos];
}

class VeiculoErrorState extends VeiculoState {
  final String message;
  const VeiculoErrorState(this.message);
  @override
  List<Object> get props => [message];
}