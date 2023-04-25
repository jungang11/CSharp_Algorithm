using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public Node root;  // root 노드를 알아야 어느 방향으로 갈지 정할 수 있음
        
        public BinarySearchTree()   // 생성자
        {
            this.root = null; // 처음엔 루트 없이 시작
        }

        // 데이터 추가
        public void Add(T item)
        {
            // 새로운 노드 추가
            Node newNode = new Node(item, null, null, null);

            // 맨 처음에는 root가 null인 상태로 시작
            if(root == null)
            {
                root = newNode;
                return;
            }

            // root가 null이 아닌 경우 진행
            Node current = root;
            while(current != null)
            {
                // 비교해서 더 작은 경우 왼쪽으로 감
                if(item.CompareTo(current.Item) < 0)
                {
                    // current가 왼쪽 자식이 있는 경우
                    if(current.Left != null)
                    {
                        // current의 왼쪽 자식과 비교하기 위해 current를 current.left로 설정
                        current = current.Left;
                    }
                    // 왼쪽 자리에 아무것도 없을 때 (current의 left가 없을 경우)
                    else
                    {
                        // 그 자리에 추가하면 됨
                        current.Left = newNode;
                        newNode.Parent = current;
                        return;
                    }
                }
                // 비교해서 더 큰 경우 오른쪽으로 감
                else if(item.CompareTo(current.Item) > 0)
                {
                    // current가 오른쪽 자식이 있는 경우
                    if (current.Right != null)
                    {
                        // 오른쪽 자식과 비교하기 위해 current 를 current.Right로 설정
                        current = current.Right;
                    }
                    else
                    {
                        // 오른쪽 자리가 비어있을 경우
                        current.Right = newNode;
                        newNode.Parent = current;
                        return;
                    }
                }
                // 동일한 데이터가 들어온 경우
                else
                {
                    return;
                }
            }
        }

        // 데이터 제거
        // 제거가 됐으면 true, 제거가 안됐으면 false
        public bool Remove(T item)
        {
            Node findNode = FindNode(item);
            // null이면 제거할게 없음
            if(findNode == null)
            {
                return false;
            }
            else
            {
                // 찾았으면 노드를 지우는 함수
                EraseNode(findNode);
                return true;
            }
        }

        // 탐색가능 한지 확인
        public bool TryGetValue(T item, out T outValue)
        {
            Node findNode = FindNode(item);

            // 탐색 노드가 null => root가 null 이거나 데이터가 탐색 불가일 경우
            if(findNode == null)
            {
                outValue = default(T);
                return false;
            }
            else
            {
                outValue = findNode.Item;
                return true;
            }
        }

        // 노드 탐색
        private Node FindNode(T item)
        {
            // 비어있을 경우 탐색 불가
            if (root == null)
                return null;

            Node current = root;
            while (current != null)
            {
                // 현재 노드의 값이 탐색중인 값보다 작은 경우
                if (item.CompareTo(current.Item) < 0)
                {
                    // 왼쪽 자식부터 다시 탐색
                    current = current.Left;
                }
                // 현재 노드의 값이 탐색중인 값보다 큰 경우
                else if (item.CompareTo(current.Item) > 0)
                {
                    // 오른쪽 자식부터 다시 탐색
                    current = current.Right;
                }
                // 현재 노드의 값이 탐색중인 값과 같은 경우 => 함수 목적
                else
                {
                    // 찾음
                    return current;
                }
            }
            // 반복을 끝냈지만 데이터를 찾지 못했을 경우
            return null;
        }

        // 노드 제거 3가지 상황 구분
        // 1. 노드의 자식이 없을 경우
        // 2. 노드의 자식이 1개 있을 경우
        // 3. 노드의 자식이 2개 있을 경우
        private void EraseNode(Node node)
        {
            // 1. 노드의 자식이 없을 경우 => 그냥 삭제
            if (node.HasNoChild)
            {
                if (node.IsLeftChild)
                    node.Parent.Left = null;
                else if (node.IsRightChild)
                    node.Parent.Right = null;
                else // if(node.IsRootNode)
                    root = null;
            }
            // 2. 노드의 자식이 1개 있을 경우 => 해당 자식 노드를 삭제할 노드의 위치로 끌어올림
            else if(node.HasLeftChild ||  node.HasRightChild)
            {
                Node parent = node.Parent;  // 현재 노드의 부모 노드
                Node child = node.HasLeftChild ? node.Left : node.Right; // 현재 노드의 자식 노드

                // 현재 노드가 왼쪽 자식이었을 경우
                if (node.IsLeftChild)
                {
                    parent.Left = child;    // 왼쪽 노드를 부모의 왼쪽노드로 옮긴다
                    child.Parent = parent;
                }
                else if (node.IsRightChild)
                {
                    parent.Right = child;
                    child.Parent = parent;
                }
                else // if(node.IsRootNode) 현재 노드가 루트노드일 경우
                {
                    root = child;   // 삭제 노드의 자식 노드를 루트 노드로 변경
                    child.Parent = null;    // 루트 노드가 되었으니 부모 노드를 null로 변경
                }
            }
            // 3. 자식 노드가 2개 있을 경우
            // => 왼쪽 자식들 중 가장 큰 값(or 오른쪽 자식들 중 가장 작은 값)으로 자신의 자리를 채움
            else // if(node.HasBothChild)
            {
                // 왼쪽이나 오른쪽 두가지 경우 모두 사용 가능
                Node replaceNode = node.Left; // 그나마 더 큰 노드를 찾기위한 replaceNode
                while(replaceNode.Right != null)
                {
                    replaceNode = replaceNode.Right;
                }
                // replaceNode를 찾은 후
                node.Item = replaceNode.Item;
                // 복사하는 replaceNode 를 지워줌
                EraseNode(replaceNode);
            }
        }

        public void Clear()
        {
            root = null;
        }

        // 입력 매개변수 노드가 없을 경우 루트노드 출력
        public void Print()
        {
            Print(root);
        }

        // 중위연산 출력
        public void Print(Node node)
        {
            Console.WriteLine(node.Item);
            if (node.HasLeftChild) Print(node.Left);
            if (node.HasRightChild) Print(node.Right);
        }

        public class Node
        {
            private T item; // 비교가 가능한 데이터여야함 => IComparable<T>
            private Node parent;
            private Node left;
            private Node right;

            public Node(T item, Node parent, Node left, Node right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            public T Item { get { return item; } set { item = value; } }
            public Node Parent { get { return parent; } set { parent = value; } }
            public Node Left { get { return left; } set { left = value; } }
            public Node Right { get { return right; } set { right = value; } }

            // node가 왼쪽 자식인지 오른쪽 자식인지 루트인지 확인하기 위한 프로퍼티
            public bool IsRootNode { get { return parent == null; } }   // 부모가 없는 노드는 root 노드
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public bool HasNoChild { get { return left == null && right == null; } } // 자식이 없는 경우
            public bool HasLeftChild { get { return left != null && right == null; } } // 자식이 왼쪽만 있는 경우
            public bool HasRightChild { get { return left == null && right != null; } } // 자식이 오른쪽만 있는 경우
            public bool HasBothChild { get { return left != null && right != null; } } // 자식이 둘 다 있는 경우
        }
    }
}
