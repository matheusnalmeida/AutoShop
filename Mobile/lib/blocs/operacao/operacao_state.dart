part of 'operacao_bloc.dart';

abstract class OperacaoState extends Equatable {
  const OperacaoState();
  
  @override
  List<Object> get props => [];
}

class OperacaoInitial extends OperacaoState {}

class LoadingState extends OperacaoState {
  const LoadingState();
  @override
  List<Object> get props => [];
}

class LoadedSucessState extends OperacaoState {
  final List<Operacao> operacoes;
  const LoadedSucessState(this.operacoes);
  @override
  List<Object> get props => [operacoes];
}

class ErrorState extends OperacaoState {
  final String message;
  const ErrorState(this.message);
  @override
  List<Object> get props => [message];
}