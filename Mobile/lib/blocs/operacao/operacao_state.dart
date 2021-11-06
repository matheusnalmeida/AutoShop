part of 'operacao_bloc.dart';

abstract class OperacaoState extends Equatable {
  const OperacaoState();
  
  @override
  List<Object> get props => [];
}

class OperacaoInitial extends OperacaoState {}

class OperacaoLoadingState extends OperacaoState {
  const OperacaoLoadingState();
  @override
  List<Object> get props => [];
}

class OperacaoLoadedSucessState extends OperacaoState {
  final List<Operacao> operacoes;
  const OperacaoLoadedSucessState(this.operacoes);
  @override
  List<Object> get props => [operacoes];
}

class OperacaoErrorState extends OperacaoState {
  final String message;
  const OperacaoErrorState(this.message);
  @override
  List<Object> get props => [message];
}

class OperacaoLoadedSucessCreateState extends OperacaoState {
  final List<Veiculo> veiculos;
  final List<Produto> produtos;

  const OperacaoLoadedSucessCreateState(this.veiculos, this.produtos);
  @override
  List<Object> get props => [veiculos, produtos];
}

class OperacaoLoadedErrorCreateState extends OperacaoState {
  final String message;
  const OperacaoLoadedErrorCreateState(this.message);
  @override
  List<Object> get props => [message];
}