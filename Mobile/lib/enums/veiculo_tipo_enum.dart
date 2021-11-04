// ignore_for_file: constant_identifier_names

enum VeiculoTipoEnum {
    Automovel,
    Motocicleta,
    Triciclo
}

extension VeiculoTipoEnumExtension on VeiculoTipoEnum {
  int get value {
    switch (this) {
      case VeiculoTipoEnum.Automovel:
        return 1;
      case VeiculoTipoEnum.Motocicleta:
        return 2;
      case VeiculoTipoEnum.Triciclo:
        return 3;
      default:
        return -1;
    }
  }
}