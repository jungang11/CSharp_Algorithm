using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey>
    {
        // 해시테이블은 초기 용량을 크게 잡음
        private const int DefaultCapacity = 1000;

        // 데이터를 저장할 때 Key, Value 둘 다 저장해주는 것이 좋음
        private struct Entry
        {
            // 사용중인지 아닌지, 삭제됐던 자리인지 상태 열거형
            public enum State { None, Using, Deleted }

            public State state;
            public int hashCode;
            public TKey key;
            public TValue value;
        }

        private Entry[] table;

        public Dictionary()
        {
            table = new Entry[DefaultCapacity];
        }

        // 초기화 함수
        public void Clear()
        {
            table = new Entry[DefaultCapacity];
        }

        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                // key값의 value를 찾아서 반환
                if (TryGetValue(key, out value))
                    return value;
                // key값을 찾지 못했을 경우 KeyNotFoundException
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                // OverrideExist : 덮어쓰기를 통해 key값의 value값을 새로 덮어쓴다
                TryInsert(key, value, InsertionBehavior.OverrideExist);
            }

            /*// 탐색해서 값을 반환하는 방법 => 모듈화가 되어서 간소화
            get
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    // 3. 겹친 위치의 키가 그 자리의 키와 같을 경우 => 반환
                    if (key.Equals(table[index].key))
                    {
                        return table[index].value;
                    }
                    // 사용중이 아닌걸 만났을 때 -> 잘못 만난것
                    if (table[index].state == Entry.State.None)
                    {
                        break;
                    }
                    index = ++index % table.Length;
                }
                throw new KeyNotFoundException();
            }
            // 탐색해서 그 위치를 바꾸는 방법
            set
            {
                // 1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                // 2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    // 3. 겹친 위치의 키가 그 자리의 키와 같을 경우 => value 값 변경
                    if (key.Equals(table[index].key))
                    {
                        table[index].value = value;
                        return;
                    }
                    // 사용중이 아닌걸 만났을 때 -> 잘못 만난것
                    if (table[index].state == Entry.State.None)
                    {
                        break;
                    }
                    index = ++index % table.Length;
                }
                throw new KeyNotFoundException();
            }*/
        }

        // 삽입할 때 상태 나누기 위한 열거형
        private enum InsertionBehavior { None, OverrideExist, ThrowOnExisting }
        // 데이터 추가
        private bool TryInsert(TKey key, TValue value, InsertionBehavior behavior)
        {
            // 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. 사용중이 아닌 index까지 다음으로 계속 이동
            while (table[index].state == Entry.State.Using)
            {
                // 겹친 위치의 키가 그 자리의 키와 같을 경우
                if (key.Equals(table[index].key))
                {
                    // Deleted된 상태도 있기 때문에 조건을 여러개로 늘림
                    switch (behavior)
                    {
                        // 덮어쓰기
                        case InsertionBehavior.OverrideExist:
                            table[index].key = key;
                            table[index].value = value;
                            return true;
                        // 오류의 상황 => 겹쳤을 때
                        case InsertionBehavior.ThrowOnExisting:
                            throw new ArgumentException();
                        // 없을 경우
                        case InsertionBehavior.None:
                        default:
                            return false;
                    }
                }
                else
                {
                    // index가 table의 크기보다 작은 경우 1을 더해주고 아니면 0으로 감
                    index = ++index % table.Length;
                }
            }
            // 3. 사용중이지 않은 index를 발견한 경우 그 위치에 저장
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using; // 사용중인 상태로 변경
            return true;
        }

        public void Add(TKey key, TValue value)
        {
            TryInsert(key, value, InsertionBehavior.ThrowOnExisting);
        }

        public bool TryAdd(TKey key, TValue value)
        {
            return TryInsert(key, value, InsertionBehavior.None);
        }

        // 데이터 삭제
        public bool Remove(TKey key)
        {
            // 입력받은 key값의 index를 찾아옴
            int index = FindIndex(key);

            // 일치하는 key값이 없었을 경우 삭제가 불가하므로 false 반환
            if (index == -1)
            {
                return false;
            }
            // 일치하는 인덱스의 상태를 삭제(Deleted)된 상태로 바꾸고 삭제가 가능했으니 true 반환
            else
            {
                table[index].state = Entry.State.Deleted;
                return true;
            }

            /*// 1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            // 2. key 값과 동일한 데이터를 찾을 때까지 index 증가
            while (table[index].state == Entry.State.Using)
            {
                // 3. 동일한 key값을 찾으면 삭제 => 삭제 후 흔적을 남겨야함
                if (key.Equals(table[index].key))
                {
                    // 상태를 Deleted 로 바꿔줌
                    table[index].state = Entry.State.Deleted;
                    return true;
                }
                // 동일한 key값을 찾지 못하고 비어있는 공간을 만났을 때
                if (table[index].state == Entry.State.None)
                {
                    break;
                }
                index = ++index % table.Length;
            }
            return false;*/
        }

        // 인덱스를 찾는 함수
        public int FindIndex(TKey key)
        {
            // key를 인덱스로 해싱
            int index= Math.Abs(key.GetHashCode() % table.Length);

            // 사용중이 아닌 index가 나올 때까지 진행
            while (table[index].state == Entry.State.Using)
            {
                // 키의 값이 일치할 경우 그 위치의 index를 반환
                if (key.Equals(table[index].key))
                {
                    return index;
                }
                // 일치하지 않을 경우 index 증가
                index = ++index % table.Length;
            }
            // index를 찾지 못했을 경우
            return -1;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = FindIndex(key);

            // 찾지 못해서 index가 -1일 경우
            if(index == -1)
            {
                value = default(TValue);
                return false;
            }
            // 찾았을 경우
            else
            {
                // 출력용 데이터 value에 위치의 value값 대입
                value = table[index].value;
                return true;
            }
        }

        // key 데이터가 테이블에 들어있는지 확인하는 함수
        public bool ContainsKey(TKey key)
        {
            // 탐색된 데이터가 없으면 TryGetValue는 false를 반환, 있으면 true를 반환
            return TryGetValue(key, out var value);
        }
    }
}
