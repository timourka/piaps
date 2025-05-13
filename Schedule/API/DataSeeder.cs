using Models;
using Repositories.Interfaces;

public class DataSeeder
{
    private readonly IRepository<JobTitle> _jobTitleRepo;
    private readonly IRepository<Worker> _workerRepo;
    private readonly IRepository<Department> _departmentRepo;
    private readonly IRepository<Reception> _receptionRepo;
    private readonly IRepository<Holiday> _holidayRepo;

    public DataSeeder(
        IRepository<JobTitle> jobTitleRepo,
        IRepository<Worker> workerRepo,
        IRepository<Department> departmentRepo,
        IRepository<Reception> receptionRepo,
        IRepository<Holiday> holidayRepo)
    {
        _jobTitleRepo = jobTitleRepo;
        _workerRepo = workerRepo;
        _departmentRepo = departmentRepo;
        _receptionRepo = receptionRepo;
        _holidayRepo = holidayRepo;
    }

    public async Task SeedAsync()
    {
        // Удаление в нужном порядке (зависимости!)
        var receptions = await _receptionRepo.GetAllAsync();
        foreach (var r in receptions)
            await _receptionRepo.DeleteAsync(r);
        await _receptionRepo.SaveAsync();

        var departments = await _departmentRepo.GetAllAsync();
        foreach (var d in departments)
            await _departmentRepo.DeleteAsync(d);
        await _departmentRepo.SaveAsync();

        var workers = await _workerRepo.GetAllAsync();
        foreach (var w in workers)
            await _workerRepo.DeleteAsync(w);
        await _workerRepo.SaveAsync();

        var jobTitles = await _jobTitleRepo.GetAllAsync();
        foreach (var jt in jobTitles)
            await _jobTitleRepo.DeleteAsync(jt);
        await _jobTitleRepo.SaveAsync();

        var holidays = await _holidayRepo.GetAllAsync();
        foreach (var h in holidays)
            await _holidayRepo.DeleteAsync(h);
        await _holidayRepo.SaveAsync();

        // 1. Должности
        var doctor = new JobTitle { name = "Doctor" };
        var nurse = new JobTitle { name = "Nurse" };
        await _jobTitleRepo.AddAsync(doctor);
        await _jobTitleRepo.AddAsync(nurse);
        await _jobTitleRepo.SaveAsync();
        doctor = await _jobTitleRepo.GetByIdAsync(doctor.id);
        nurse = await _jobTitleRepo.GetByIdAsync(nurse.id);

        // 2. Сотрудники с отпусками
        var vacation = new Vacation
        {
            days = new List<DayOff>
        {
            new DayOff { date = DateOnly.FromDateTime(DateTime.Today.AddDays(5)) }
        }
        };

        var worker1 = new Worker
        {
            name = "Alice Smith",
            login = "alice",
            password = "password123",
            jobTitle = doctor,
            vacations = new List<Vacation> { vacation }
        };

        var worker2 = new Worker
        {
            name = "Bob Johnson",
            login = "bob",
            password = "secure456",
            jobTitle = nurse,
            vacations = new List<Vacation>()
        };

        await _workerRepo.AddAsync(worker1);
        await _workerRepo.AddAsync(worker2);
        await _workerRepo.SaveAsync();
        worker1 = await _workerRepo.GetByIdAsync(worker1.id);
        worker2 = await _workerRepo.GetByIdAsync(worker2.id);

        // 3. Отделение с графиком
        var schedule = new List<WorkSchedule4Day>
    {
        new WorkSchedule4Day { isWorking = true, startOfWork = new TimeOnly(8, 0), endOfWork = new TimeOnly(16, 0) }, // Пн
        new WorkSchedule4Day { isWorking = true, startOfWork = new TimeOnly(8, 0), endOfWork = new TimeOnly(16, 0) }, // Вт
        new WorkSchedule4Day { isWorking = true, startOfWork = new TimeOnly(8, 0), endOfWork = new TimeOnly(16, 0) }, // Ср
        new WorkSchedule4Day { isWorking = true, startOfWork = new TimeOnly(8, 0), endOfWork = new TimeOnly(16, 0) }, // Чт
        new WorkSchedule4Day { isWorking = true, startOfWork = new TimeOnly(8, 0), endOfWork = new TimeOnly(16, 0) }, // Пт
        new WorkSchedule4Day { isWorking = false },
        new WorkSchedule4Day { isWorking = false }
    };

        var department = new Department
        {
            name = "Therapy",
            workSchedules = schedule,
            workers = new List<Worker> { worker1, worker2 }
        };

        await _departmentRepo.AddAsync(department);
        await _departmentRepo.SaveAsync();
        department = await _departmentRepo.GetByIdAsync(department.id);

        // 4. Приём
        var reception = new Reception
        {
            time = TimeSpan.FromMinutes(30),
            requiredPersonnel = new List<JobTitle> { doctor, nurse },
            department = department
        };

        await _receptionRepo.AddAsync(reception);
        await _receptionRepo.SaveAsync();

        // 5. Праздник
        var holiday = new Holiday
        {
            name = "New Year",
            date = new DateOnly(DateTime.Today.Year, 1, 1)
        };

        await _holidayRepo.AddAsync(holiday);
        await _holidayRepo.SaveAsync();
    }

}
