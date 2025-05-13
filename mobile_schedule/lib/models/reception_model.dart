class Reception {
  int id;
  String date;
  String startTime;
  String endTime;
  String departmentName;
  String time;

  Reception({
    required this.id,
    required this.date,
    required this.startTime,
    required this.endTime,
    required this.departmentName,
    required this.time,
  });

  factory Reception.fromJson(Map<String, dynamic> json) {
    return Reception(
      id: json['id'],
      date: json['date'],
      startTime: json['startTime'],
      endTime: json['endTime'],
      departmentName: json['department']['name'],
      time: json['time'],
    );
  }
}
