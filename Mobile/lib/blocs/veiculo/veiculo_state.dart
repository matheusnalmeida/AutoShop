part of 'veiculo_bloc.dart';

abstract class VeiculoState extends Equatable {
  const VeiculoState();
  
  @override
  List<Object> get props => [];
}

class VeiculoInitial extends VeiculoState {}

class EmptyState extends VeiculoState {
  @override
  List<Object> get props => [];
}

class InitialState extends VeiculoState {
  const InitialState();
  @override
  List<Object> get props => [];
}

class LoadingState extends VeiculoState {
  const LoadingState();
  @override
  List<Object> get props => [];
}

class LoadedSucessState extends VeiculoState {
  final List<Veiculo> veiculos;
  const LoadedSucessState(this.veiculos);
  @override
  List<Object> get props => [veiculos];
}

class ErrorState extends VeiculoState {
  final String message;
  const ErrorState(this.message);
  @override
  List<Object> get props => [message];
}