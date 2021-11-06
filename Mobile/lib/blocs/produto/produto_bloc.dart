import 'package:autoshop_application/exceptions/http_exception.dart';
import 'package:autoshop_application/models/produto/produto.dart';
import 'package:autoshop_application/repositories/repository.dart';
import 'package:bloc/bloc.dart';
import 'package:equatable/equatable.dart';

part 'produto_event.dart';
part 'produto_state.dart';

class ProdutoBloc extends Bloc<ProdutoEvent, ProdutoState> {
  final ProdutoRepository repository;

  ProdutoBloc(this.repository) : super(const ProdutoLoadingState()) {
    on<GetAllProdutosEvent>(_onPostFetched);
    on<CreateProdutoEvent>(_onCreate);
    on<UpdateProdutoEvent>(_onUpdate);
    on<DeleteProdutoEvent>(_onDelete);
  }

  Future<void> _onPostFetched(
      GetAllProdutosEvent event, Emitter<ProdutoState> emit) async {
    try {
      emit(const ProdutoLoadingState());
      var result = (await repository.fetchAllProdutos());
      return emit(ProdutoLoadedSucessState(result));
    } on HttpException catch (ex) {
      return emit(ProdutoErrorState(ex.message));
    } catch (_) {
      return emit(const ProdutoErrorState("Erro ao tentar obter os produtos!"));
    }
  }

  Future<void> _onCreate(
      CreateProdutoEvent event, Emitter<ProdutoState> emit) async {
    try {
      emit(const ProdutoLoadingState());
      var result = await repository.createProduto(event.produto);
      if (result.sucesso) {
        add(GetAllProdutosEvent());
        return;
      }
      emit(ProdutoErrorState(result.mensagens[0]));
    } on HttpException catch (ex) {
      return emit(ProdutoErrorState(ex.message));
    } catch (_) {
      return emit(const ProdutoErrorState("Erro ao tentar cadastrar produto!"));
    }
  }

  Future<void> _onUpdate(
      UpdateProdutoEvent event, Emitter<ProdutoState> emit) async {
    try {
      emit(const ProdutoLoadingState());
      var result = await repository.updateProduto(event.produto);
      if (result.sucesso) {
        add(GetAllProdutosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(ProdutoErrorState(ex.message));
    } catch (_) {
      return emit(const ProdutoErrorState("Erro ao tentar atualizar produto!"));
    }
  }

  Future<void> _onDelete(
      DeleteProdutoEvent event, Emitter<ProdutoState> emit) async {
    try {
      emit(const ProdutoLoadingState());
      var result = await repository.deleteProduto(event.produto.id!);
      if (result.sucesso) {
        add(GetAllProdutosEvent());
        return;
      }
    } on HttpException catch (ex) {
      return emit(ProdutoErrorState(ex.message));
    } catch (_) {
      return emit(const ProdutoErrorState("Erro ao tentar remover produto!"));
    }
  }
}
