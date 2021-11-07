part of 'produto_bloc.dart';

abstract class ProdutoState extends Equatable {
  const ProdutoState();
  
  @override
  List<Object> get props => [];
}

class ProdutoInitial extends ProdutoState {}

class ProdutoLoadingState extends ProdutoState {
  const ProdutoLoadingState();
  @override
  List<Object> get props => [];
}

class ProdutoLoadedSucessState extends ProdutoState {
  final List<Produto?> produtos;
  const ProdutoLoadedSucessState(this.produtos);
  @override
  List<Object> get props => [produtos];
}

class ProdutoErrorState extends ProdutoState {
  final String message;
  const ProdutoErrorState(this.message);
  @override
  List<Object> get props => [message];
}