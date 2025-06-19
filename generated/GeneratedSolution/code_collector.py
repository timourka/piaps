import os

# Настройки
extensions = [".cshtml", ".cs"]  # Какие типы файлов включать
project_root = "D:\piaps\generated\GeneratedSolution"  # Корень проекта (можно заменить на путь к вашему проекту)
output_file = "combined_code.txt"  # Имя выходного файла

def combine_project_files(root_dir, extensions, output_file):
    with open(output_file, "w", encoding="utf-8") as outfile:
        for dirpath, _, filenames in os.walk(root_dir):
            for filename in filenames:
                file_ext = os.path.splitext(filename)[1].lower()
                if file_ext in extensions:
                    file_path = os.path.join(dirpath, filename)
                    try:
                        with open(file_path, "r", encoding="utf-8", errors="ignore") as infile:
                            content = infile.read()
                            outfile.write(f"// ==== {file_path} ====\n")
                            outfile.write(content)
                            outfile.write("\n\n\n")
                    except Exception as e:
                        print(f"[Ошибка при чтении] {file_path}: {e}")

if __name__ == "__main__":
    print("Начинаю объединение файлов...")
    combine_project_files(project_root, extensions, output_file)
    print(f"Готово! Все файлы объединены в '{output_file}'")