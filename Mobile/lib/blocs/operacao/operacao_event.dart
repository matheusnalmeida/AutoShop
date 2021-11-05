part of 'operacao_bloc.dart';

abstract class OperacaoEvent extends Equatable {
  const OperacaoEvent();

  @override
  List<Object> get props => [];
}

class GetAllOperacoesEvent extends OperacaoEvent {
  @override
  List<Object> get props => [];
}

class GetOperacaoEvent extends OperacaoEvent {
  final String id;
  const GetOperacaoEvent(this.id);
  
  @override
  List<Object> get props => [id];
}

class CreateOperacaoEvent extends OperacaoEvent {
  final OperacaoCreate operacao;
  const CreateOperacaoEvent(this.operacao);

  @override
  List<Object> get props => [operacao];
}

class UpdateOperacaoEvent extends OperacaoEvent {
  final Operacao operacao;
  const UpdateOperacaoEvent(this.operacao);

  @override
  List<Object> get props => [operacao];
}

class DeleteOperacaoEvent extends OperacaoEvent {
  final Operacao operacao;

  const DeleteOperacaoEvent(this.operacao);

  @override
  List<Object> get props => [operacao];
}