using System;

namespace Carret
{
    /// <summary>
    /// Класс для вывода строки на том же месте, где она была
    /// выведена в первый раз.
    /// Удобно использовать, чтобы не плодить строки и обновлять уже существующую информацию.
    /// </summary>
    public class UpdateString
    {
        private string[] write;
        private int top = -1;
        private int latestTop = -1;
        private int latestLeft = -1;
        private bool firstWrite = true;
        /// <summary>
        /// Возвращает количество строк для вывода
        /// </summary>
        public int length { get { return write.Length; } }
        /// <summary>
        /// Конструктор объекта типа UpdateString.
        /// </summary>
        /// <param name="write">Набор строк для вывода</param>
        public UpdateString(params string[] write)
        {
            this.write = write;
        }
        /// <summary>
        /// Метод вывода строки в консоль в одном и том же месте.
        /// Первый вывод осуществляется на текущем месте каретки (с последующим её переносом на новую строку).
        /// Последующие выводы будут осуществлятся на месте первого вывода строки,
        /// после чего каретка будет возвращена на место до вызова этого метода.
        /// </summary>
        /// <param name="write">Строки для вывода</param>
        public void Write(params string[] write)
        {
            if (write.Length > 0)
                this.write = write;

            latestTop = Console.CursorTop;
            latestLeft = Console.CursorLeft;

            if (top == -1)
                top = latestTop;
            else Console.CursorTop = top;

            Console.CursorLeft = 0;

            string build = "";

            foreach (string a in this.write)
            {
                build += a;
            }

            int buffer = Console.WindowWidth;

            if (build.Length > buffer) build = build.Substring(0, buffer);
            else if (build.Length < buffer) build += new string(' ', buffer - build.Length);

            if (firstWrite)
            {
                Console.WriteLine(build);
                latestTop++;
                firstWrite = false;
            }
            else Console.Write(build);

            Console.CursorTop = latestTop;
            Console.CursorLeft = latestLeft;
        }
        /// <summary>
        /// Удаляет строку с заданным индексом.
        /// </summary>
        /// <param name="index">Индекс строки</param>
        public void RemoveIndex(int index)
        {
            if (index >= 0 && index < write.Length)
            {
                for (int i = index; i < write.Length - 1; i++)
                {
                    write[i] = write[i + 1];
                }
                Array.Resize(ref write, write.Length - 1);
            }
        }
        /// <summary>
        /// Очищает строку для вывода от всех элементов.
        /// </summary>
        public void Clear()
        {
            write = new string[] { };
        }
        /// <summary>
        /// Изменяет количество элементов для вывода.
        /// </summary>
        /// <param name="newSize">Новое количество элементов</param>
        public void Resize(int newSize)
        {
            Array.Resize(ref write, newSize);
        }
        /// <summary>
        /// Добавить элемент в самом начале.
        /// </summary>
        /// <param name="value">Значение для добавления в начале строки</param>
        public void AddValueFirst(string value)
        {
            Array.Resize(ref write, write.Length + 1);

            for (int i = write.Length - 1; i > 0; i--)
            {
                write[i] = write[i - 1];
            }
            write[0] = value;
        }
        /// <summary>
        /// Добавить элемент в самом конце.
        /// </summary>
        /// <param name="value">Значение для добавления в конец строки</param>
        public void AddValueLast(string value)
        {
            Array.Resize(ref write, write.Length + 1);
            write[write.Length - 1] = value;
        }
        /// <summary>
        /// Добавить элементы в самом конце.
        /// </summary>
        /// <param name="value">Значения для добавления в конце строки</param>
        public void AddValueLast(params string[] value)
        {
            if (value.Length > 0)
            {
                int lengthLast = write.Length;
                Array.Resize(ref write, lengthLast + value.Length);
                for (int i = 0; i + lengthLast < write.Length; i++)
                {
                    write[i + lengthLast] = value[i];
                }
            }
        }
        /// <summary>
        /// Чтение/Запись значения с нужным индексом.
        /// </summary>
        /// <param name="index">Индекс элемента</param>
        /// <returns>подстрока с заданным индексом</returns>
        public string this[int index]
        {
            get { return write[index]; }
            set { write[index] = value; }
        }
    }
}
