package com.kangaroo.studentedu.ui.adapter

import com.chad.library.adapter.base.BaseQuickAdapter
import com.chad.library.adapter.base.module.LoadMoreModule
import com.chad.library.adapter.base.viewholder.BaseViewHolder
import com.kangaroo.studentedu.R
import kotlinx.android.synthetic.main.item_xie_home_work.view.*

/**
 * 自动生成：by WaTaNaBe on 2021-05-27 13:54
 * #写作业#
 */
class XieHomeWorkAdapter :BaseQuickAdapter<String, BaseViewHolder>(R.layout.item_xie_home_work),LoadMoreModule {
    
    override fun convert(holder: BaseViewHolder, item: String) {
        holder.itemView.kecheng.text = item
    }
    
}
