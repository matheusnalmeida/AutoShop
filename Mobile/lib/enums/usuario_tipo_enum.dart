// ignore_for_file: constant_identifier_names

enum UsuarioTipoEnum {
      Cliente,
      Vendedor,
      Administrador
}

extension UsuarioTipoEnumExtension on UsuarioTipoEnum {
  int get value {
    switch (this) {
      case UsuarioTipoEnum.Cliente:
        return 1;
      case UsuarioTipoEnum.Vendedor:
        return 2;
      case UsuarioTipoEnum.Administrador:
        return 3;
      default:
        return -1;
    }
  }
}