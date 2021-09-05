abstract class AbstractMapper<T>{
  T fromJson(Map<String, Object?> json);
  Map<String, Object?> toJson(T object);
}