# 目录
[1.时间控制](#TimeVariable)</br>
[2.多重资源管理器](#MultiAttributeResource)</br>
[3.节点图基础](#BaseNodeGraph)</br>
[4.静态对象调试](#StaticDebug)</br>
[5.层级窗口扩展](#Hierarchy)</br>

---
## 1.时间控制<a name="TimeVariable"></a>

### 生命循环器
生命控制器是时间控制的承载者，通过LifeCycle的单例可以挂载实现了ILifeCycle接口的对象，无论其是否继承自**MonoBehaviour**
</br>
LifeCycle将提供绘画帧更新和物理帧更新的函数调用，并且在达成条件时自动将对象移出监管列表。
</br></br>

### 计时器变量
DecUInt是一个实现了ILifeCycle的变量，它是对UInt的一层封装，并在每一个物理帧自减。
使用构造函数初始化的时候，需要一个GameObject物体作为挂载的对象，
一旦该对象被销毁，DecUInt也将销毁。
</br></br>
DelegateUInt在DecUInt的基础上加入了计时归零触发回调的功能，在使用构造函数初始化的时候指定回调。
同时，DelegateUInt一旦计时不为零，将无法再次设置时间，只能通过Trigger立即触发、Cancel取消触发、
Lengthen延长触发进行更改。
</br></br>
使用样例
```
DecUInt foo = new(gameObject);
foo.Set(1f);
if(foo.Zero())
{
	...Do Something.
}

DelegateUInt foo = new(gameObject,() => Debug.Log("Delegate Triggered!"));
foo.Set(1f);
foo.Cancel();
foo.Set(2f);
foo.Lengthen(1f);
foo.Trigger();
```
</br></br>

### 延时委托(重构中)
</br></br>

---
## 2.多重资源属性<a name ="MultiAttributeResource"></a>

### 多重属性资源
MultiAttributeResource是一个继承自ScriptObject的资源文件，它可以包含若干个String、Int、Bool、Float变量。
在运行过程中，只需要对于MAR对象使用GetString方法就能获取指定变量的值。
</br>
在Inspector中只能查看它包含的属性，如果需要编辑，请在MikanLab/多重属性资源编辑器界面中进行。
</br></br>

---
## 3.节点图基础<a name ="BaseNodeGraph"></a>

### 节点图资源
NodeGraph是节点图基类，存储了节点的信息，包括每一个节点的位置、类型以及连接状况。
</br>
使用Execute方法进行遍历，这需要您自己定义遍历的方式。
</br></br>

### 可视化编辑
节点图采用GraphView进行可视化编辑,NodeGraphElement是对GraphView的一层封装，提供了从文件到视图和从视图到文件的转换
</br>
允许右键创建新的节点，通过SearchTree进行，具体的搜索方式见下文。
</br></br>

### 节点属性
[UniversalUsed]表示该节点及其所有继承类永远会被搜索到。
</br>
[UsedFor]表示该节点及其继承类（可选）会被指定的节点图资源类型搜索到。

### 节点图属性
[CountLimit]表示该节点图中指定类型的节点的数量应该在Min-Max之间，数量 < Min值将会自动添加，>= Max值将无法被搜索到。
</br></br>

### 随机表（施工中）
RandomPool是一个继承自NodeGraph的资源文件，它利用节点图的形式表现一个随机过程的输入、判断、随机生成以及输出</br>
其中，节点的类别分为

<ul>
<li> <strong>输入节点</strong></br>
输入节点用于设置随机过程的参数，每一个节点图有且仅有一个输入节点
</li>

<li> <strong>输出节点</strong></br>
输出节点用于表示随机过程的结束，并且返回随机过程的结果列表，每一个节点图有且仅有一个输出节点</br>
(虽然输出的结果可以为列表，但是在随机过程中不可以出现多个分支被同时执行的情况)</br>
</li>

<li> <strong>物品节点</strong></br>
物品节点用于向结果列表里面加入单/多项结果，具有权重
</li>

<li> <strong>修饰节点</strong></br>
修饰节点用于对其指向的后方节点的生成方式进行修饰，包括数量、状态、执行次数
</li>

<li> <strong>条件节点</strong></br>
条件节点用于检测是否满足一个条件表达式，若满足，则将其指向的物品节点加入权重池中，否则不加入
</li>

</ul>
</br></br>

---
## 4.静态对象调试<a name ="StaticDebug"></a>

### 标记类
打上[TrackStatic]的类将纳入静态变量监视列表，类中的静态对象可以在MikanLab/静态变量监视器中查看。
注意，类带有[TrackStatic]特性是被监视的先决条件，如何没有此特性，就算给静态成员施以对应特性也不会加入监视列表中。
</br></br>

### 标记成员
对于静态字段和静态属性，打上[EditableStatic]将显示可编辑的数值，打上[ReadonlyStatic]将显示仅可读的数值。
对于静态方法，只有没有参数的静态方法可以打上[VoidStaticMethod]，此时可以在监听器中通过按钮进行显式调用。
</br></br>

## 5.层级窗口拓展<a name ="Hierarchy"></a>

### 分割线
右键打开菜单，并选中MikanLab/分割线以在层级窗口中添加一个提供视觉上划分区域的分割线。
实际上带有SeparatorComponent组件的GameObject在层级窗口将被此种方式覆盖绘制，可以通过修改其属性控制颜色和文字显示。