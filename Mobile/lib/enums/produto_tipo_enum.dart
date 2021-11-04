// ignore_for_file: constant_identifier_names

enum ProdutoTipoEnum {
    Acessorio,
    Seguro,
    GarantiaEstendida,
    Manutencao
}
extension ProdutoTipoEnumExtension on ProdutoTipoEnum {
  int get value {
    switch (this) {
      case ProdutoTipoEnum.Acessorio:
        return 1;
      case ProdutoTipoEnum.Seguro:
        return 2;
      case ProdutoTipoEnum.GarantiaEstendida:
        return 3;
      case ProdutoTipoEnum.Manutencao:
        return 4;
      default:
        return -1;
    }
  }
}