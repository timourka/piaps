import 'package:flutter/material.dart';

import '../models/reception_model.dart';
import '../services/api_service.dart';

class ReceptionScreen extends StatefulWidget {
  final String sid;
  final String login;

  ReceptionScreen({required this.sid, required this.login});

  @override
  _ReceptionScreenState createState() => _ReceptionScreenState();
}

class _ReceptionScreenState extends State<ReceptionScreen> {
  final _apiService = ApiService();
  List<Reception> _receptions = [];

  @override
  void initState() {
    super.initState();
    _loadReceptions();
  }

  void _loadReceptions() async {
    try {
      final receptions = await _apiService.getReceptions(widget.sid, widget.login);
      setState(() {
        _receptions = receptions;
      });
    } catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text('Failed to load receptions')));
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Schedule')),
      body: ListView.builder(
        itemCount: _receptions.length,
        itemBuilder: (context, index) {
          final reception = _receptions[index];
          return ListTile(
            title: Text(reception.date),
            subtitle: Text('Start: ${reception.startTime} - End: ${reception.endTime}'),
            trailing: Text(reception.departmentName),
          );
        },
      ),
    );
  }
}
