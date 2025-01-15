# 目录
[1.时间控制](#TimeControl)</br>
[2.多重资源管理器](#MultiAttributeResource)</br>
[3.节点图拓展](#NodeGraphExtension)</br>
[4.调试](#Debug)</br>
[5.层级窗口扩展](#Hierarchy)</br>

---
## 1.时间控制<a id="TimeControl"></a>

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

### 任务进度条
基于UniTask的任务布置器，通过将任务添加到进度条中，使用Start方法启动执行，可以通过公开的数据接口获取实施进度
以及当前执行的任务描述
</br></br>
```
TaskProgress foo = new("完成任务...");
foo.AddTask(func1,"进行XXX",1);
foo.AddTask(func2,"进行XX",2);
foo.AddTask(func3,"进行X",3);
foo.Start();
foo.ProgressFloat => 进度，为介于0-1之间的数
foo.CurrentTaskDescription => 正在执行的任务描述
foo.LeftTask => 剩余未完成的任务项，包括正在执行的
```
每个任务推进的进度由权重决定，通过AddTask的第三个参数指定
</br></br>

---
## 2.多重资源属性<a id ="MultiAttributeResource"></a>

### 多重属性资源
MultiAttributeResource是一个继承自ScriptObject的资源文件，它可以包含若干个String、Int、Bool、Float变量。
在运行过程中，只需要对于MAR对象使用GetString方法就能获取指定变量的值。
</br>
在Inspector中只能查看它包含的属性，如果需要编辑，请在MikanLab/多重属性资源编辑器界面中进行。
</br></br>

---
## 3.节点图拓展<a id ="NodeGraphExtension"></a>

此部分内容基于[NodeGraphTemplate仓库](https://github.com/ZundaDD/NodeGraphTemplate "github仓库")，作为包的依赖项
请先行下载</br>

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
## 4.调试<a id ="Debug"></a>

### 调试类
打上[DebugClass]的类将纳入调试器，类中的调试方法可以在MikanLab/调试器中查看。
注意，类带有[DebugClass]特性是被监视的先决条件，如何没有此特性，就算给静态成员施以对应特性也不会加入监视列表中。
</br></br>

### 调试方法
对于静态无参方法，可以打上[DebugMethod]，此时可以在调试器中通过按钮进行显式调用。
</br></br>

## 5.层级窗口拓展<a id ="Hierarchy"></a>

### 分割线
右键打开菜单，并选中MikanLab/分割线以在层级窗口中添加一个提供视觉上划分区域的分割线。
实际上带有SeparatorComponent组件的GameObject在层级窗口将被此种方式覆盖绘制，可以通过修改其属性控制颜色和文字显示。