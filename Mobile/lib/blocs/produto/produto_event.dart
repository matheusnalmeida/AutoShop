part of 'produto_bloc.dart';

abstract class ProdutoEvent extends Equatable {
  const ProdutoEvent();

  @override
  List<Object> get props => [];
}

class GetAllProdutosEvent extends ProdutoEvent {
  @override
  List<Object> get props => [];
}

class GetProdutoEvent extends ProdutoEvent {
  final String id;
  const GetProdutoEvent(this.id);
  
  @override
  List<Object> get props => [id];
}

class CreateProdutoEvent extends ProdutoEvent {
  final Produto produto;
  const CreateProdutoEvent(this.produto);

  @override
  List<Object> get props => [produto];
}

class UpdateProdutoEvent extends ProdutoEvent {
  final Produto produto;
  const UpdateProdutoEvent(this.produto);

  @override
  List<Object> get props => [produto];
}

class DeleteProdutoEvent extends ProdutoEvent {
  final Produto produto;

  const DeleteProdutoEvent(this.produto);

  @override
  List<Object> get props => [produto];
}