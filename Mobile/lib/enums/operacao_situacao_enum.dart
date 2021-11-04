// ignore_for_file: constant_identifier_names

enum OperacaoSituacaoEnum {
    EmAnalise,
    Aprovada,
    Recusada,
    Cancelada
}

extension OperacaoSituacaoEnumExtension on OperacaoSituacaoEnum {
  int get value {
    switch (this) {
      case OperacaoSituacaoEnum.EmAnalise:
        return 1;
      case OperacaoSituacaoEnum.Aprovada:
        return 2;
      case OperacaoSituacaoEnum.Recusada:
        return 3;
      case OperacaoSituacaoEnum.Cancelada:
        return 4;
      default:
        return -1;
    }
  }
}