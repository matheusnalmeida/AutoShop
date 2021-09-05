import 'package:auto_shop/Shared/Mapper/abstract_mapper.dart';
import 'package:auto_shop/src/Models/vehicle_model.dart';

class VehicleMapper implements AbstractMapper<Vehicle>{
  
  static final VehicleMapper _vehicleMapper = VehicleMapper._internal();

  factory VehicleMapper() {
    return _vehicleMapper;
  }

  VehicleMapper._internal();

  @override
  Vehicle fromJson(Map<String, Object?> json) {
    return Vehicle(
        id: json['id'] as String?,
        name: json['name'] as String,
        description: json['description'] as String
      );
  }

  @override
  Map<String, Object?> toJson(Vehicle object) {
    return {
      'id': object.id,
      'name': object.name,
      'description': object.description
    };
  }
}