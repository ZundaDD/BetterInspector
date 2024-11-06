# 目录
[1.变量](#Variable)</br>
[2.资源文件](#ResourceAsset)</br>
[3.属性](#Attribute)</br>

---
## 变量<a name="Variable"></a>

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

---
## 资源文件<a name ="ResourceAsset"></a>

### 多重属性资源
MultiAttributeResource是一个继承自ScriptObject的资源文件，它可以包含若干个String、Int、Bool、Float变量。
在运行过程中，只需要对于MAR对象使用GetString方法就能获取指定变量的值。
</br>
在Inspector中只能查看它包含的属性，如果需要编辑，请在MikanLab/多重属性资源编辑器界面中进行。
</br></br>

### 随机表（施工中）
RandomPool是一个继承自ScriptObject的资源文件，它利用节点图的形式表现一个随机过程的输入、判断、随机生成以及输出</br>
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
## 属性<a name ="Attribute"></a>

### 静态变量监听
打上[TrackStatic]的类将纳入静态变量监视列表，类中的静态对象可以在KikanLab/静态变量监视器中查看。
</br>
对于静态字段和静态属性，打上[EditableStatic]将显示可编辑的数值，打上[ReadonlyStatic]将显示仅可读的数值。
对于静态方法，只有没有参数的静态方法可以打上[VoidStaticMethod]，此时可以在监听器中通过按钮进行显式调用。
</br></br>