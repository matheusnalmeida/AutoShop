part of 'produto_bloc.dart';

abstract class ProdutoState extends Equatable {
  const ProdutoState();
  
  @override
  List<Object> get props => [];
}

class ProdutoInitial extends ProdutoState {}

class LoadingState extends ProdutoState {
  const LoadingState();
  @override
  List<Object> get props => [];
}

class LoadedSucessState extends ProdutoState {
  final List<Produto> produtos;
  const LoadedSucessState(this.produtos);
  @override
  List<Object> get props => [produtos];
}

class ErrorState extends ProdutoState {
  final String message;
  const ErrorState(this.message);
  @override
  List<Object> get props => [message];
}