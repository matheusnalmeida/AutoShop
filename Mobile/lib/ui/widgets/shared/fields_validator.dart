class FieldsValidator{
  static bool isCreate(Map<String, dynamic> formData){
    return formData.containsKey("isCreate") && formData["isCreate"];
  } 
  static bool isDetails(Map<String, dynamic> formData){
    return formData.containsKey("isDetails") && formData["isDetails"];
  } 
} 