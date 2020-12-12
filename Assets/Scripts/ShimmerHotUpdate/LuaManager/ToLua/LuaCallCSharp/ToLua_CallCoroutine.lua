print("**************toLua访问C#的协程******************")

--toLua提供给我们了一些方便开启协程的方法

local coDelay = nil
StartDelay = function()
    --toLua提供给我们的开启协程的方法
    coDelay = StartCoroutine(Delay)
end

Delay = function()
    local c = 1
    while true do
        --可直接使用类似Unity中的协程相关的方法
        WaitForSeconds(1)
        --Yield(0)
        --WaitForFixedUpdate()
        --WaitForEndOfFrame()
        --Yield(异步加载返回值)
        print("Count:" .. c)
        c = c + 1

        if c > 5 then
            StopDelay()
            break
        end
    end
end

StopDelay = function()
    StopCoroutine(coDelay)
    coDelay = nil
end

StartDelay()