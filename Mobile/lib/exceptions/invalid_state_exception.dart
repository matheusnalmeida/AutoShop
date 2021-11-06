class InvalidStateException implements Exception {
  final String message;

  InvalidStateException(this.message); 

  @override
  String toString() {
    return message;
  }
}